using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using SymbolComputations.Reducers;
using SymbolComputations.Symbols;

namespace SymbolComputations.Plots
{
    public class Plots : Game
    {
        private readonly Symbol _renderSymbol;
        private readonly Symbol[] _renderFunctions;
        private readonly ReductionContext _context;
        private readonly int _maxWidth;
        private readonly int _maxHeight;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _startPoint;
        private Vector2 _rotation;
        private float _step;
        private Dictionary<(int, float, float), Symbol> _hash;

        public Plots(Symbol[] renderFunctions, ReductionContext context, int maxWidth = 1000, int maxHeight = 1000)
        {
            _renderFunctions = renderFunctions;
            _context = context;
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;

            _hash = new Dictionary<(int, float, float), Symbol>();
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _startPoint = Vector2.Zero;
            _rotation = Vector2.Zero;
            _step = 50;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                keyboardState.IsKeyDown(Keys.Escape))
                Exit();


            float
                delta = gameTime.GetElapsedSeconds(),
                rotationSpeed = delta * MathF.PI / 24f,
                movementSpeed = delta * _step * 10f;

            RotateCamera(keyboardState, rotationSpeed);
            MoveCamera(keyboardState, movementSpeed);

            base.Update(gameTime);
        }

        private void MoveCamera(KeyboardState keyboardState, float movementSpeed)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                _startPoint -= new Vector2(movementSpeed, 0);
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                _startPoint += new Vector2(movementSpeed, 0);
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                _startPoint -= new Vector2(0, movementSpeed);
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                _startPoint += new Vector2(0, movementSpeed);
            }
        }

        private void RotateCamera(KeyboardState keyboardState, float rotationSpeed)
        {
            if (keyboardState.IsKeyDown(Keys.H))
            {
                _rotation -= new Vector2(rotationSpeed, 0f);
            }

            if (keyboardState.IsKeyDown(Keys.K))
            {
                _rotation += new Vector2(rotationSpeed, 0);
            }

            if (keyboardState.IsKeyDown(Keys.J))
            {
                _rotation += new Vector2(0, rotationSpeed);
            }

            if (keyboardState.IsKeyDown(Keys.U))
            {
                _rotation -= new Vector2(0, rotationSpeed);
            }
        }

        private static Vector2?[,] ProjectMatrix(Vector3?[,] matrix, Vector2 rotation, Vector3 rotationPoint)
        {
            var result = new Vector2?[matrix.GetLength(0), matrix.GetLength(1)];

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (!matrix[i, j].HasValue) continue;

                    result[i, j] = ProjectMatrix(rotation, rotationPoint, matrix[i, j].Value);
                }
            }

            return result;
        }

        private static Vector2 ProjectMatrix(Vector2 rotation, Vector3 rotationPoint, Vector3 point)
        {
            (float relevantX, float relevantY, float relevantZ) = point - rotationPoint;
            (float rotationX, float rotationY) = rotation;

            return new Vector2
            {
                X = relevantX * MathF.Cos(rotationX) + relevantZ * MathF.Sin(rotationX),
                Y = relevantY * MathF.Cos(rotationY) + relevantZ * MathF.Sin(rotationY)
            };
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            int width = (int) MathF.Abs(GraphicsDevice.Viewport.Width / _step / MathF.Cos(_rotation.X)) + 2,
                height = (int) MathF.Abs(GraphicsDevice.Viewport.Height / _step / MathF.Cos(_rotation.Y)) + 2;

            if (width > _maxWidth)
            {
                width = _maxWidth;
            }

            if (height > _maxHeight)
            {
                height = _maxHeight;
            }

            Vector3?[][,] points = CalculatePoints(width, height, _startPoint).ToArray();
            IEnumerable<Vector2?[,]> projections = points
                .Select(m => ProjectMatrix(m, _rotation, new Vector3(_startPoint, 0)));

            DrawPoints(projections.ToArray(), points);

            base.Draw(gameTime);
        }

        private void DrawPoints(IReadOnlyList<Vector2?[,]> projections, IReadOnlyList<Vector3?[,]> originalPoints)
        {
            var screenCenter = new Vector2(
                GraphicsDevice.Viewport.Width / 2f,
                GraphicsDevice.Viewport.Height / 2f
            );

            _spriteBatch.Begin();

            for (var projectionNumber = 0; projectionNumber < projections.Count; projectionNumber++)
            {
                Vector2?[,] projection = projections[projectionNumber];
                float
                    minZ = originalPoints[projectionNumber]
                        .Cast<Vector3?>()
                        .Min(v => v?.Z ?? 0f),
                    maxZ = originalPoints[projectionNumber]
                        .Cast<Vector3?>()
                        .Max(v => v?.Z ?? 0f);


                for (var i = 0; i < projection.GetLength(0); i++)
                for (var j = 0; j < projection.GetLength(1); j++)
                    DrawPoint(originalPoints[projectionNumber], projection, i, j, maxZ, minZ, screenCenter);
            }

            _spriteBatch.End();
        }

        private void DrawPoint(Vector3?[,] originalPoints, Vector2?[,] projection, int i, int j, float maxZ, float minZ,
            Vector2 screenCenter)
        {
            if (!projection[i, j].HasValue) return;

            var neighbors = new List<(int, int)>();

            if (i > 0)
            {
                neighbors.Add((i - 1, j));
            }

            if (j > 0)
            {
                neighbors.Add((i, j - 1));
            }

            float
                z = MathF.Min(maxZ, MathF.Max(originalPoints[i, j].Value.Z, minZ));

            Color
                minColor = Color.Yellow,
                maxColor = Color.Orange,
                color = new Color
                {
                    R = (byte) (minColor.R + (maxColor.R - minColor.R) * (z - minZ) / maxZ),
                    G = (byte) (minColor.G + (maxColor.G - minColor.G) * (z - minZ) / maxZ),
                    B = (byte) (minColor.B + (maxColor.B - minColor.B) * (z - minZ) / maxZ)
                };

            foreach ((int ii, int jj) in neighbors)
            {
                if (!projection[ii, jj].HasValue) continue;

                _spriteBatch.DrawLine(
                    screenCenter + projection[ii, jj].Value,
                    screenCenter + projection[i, j].Value,
                    color
                );
            }

            _spriteBatch.DrawPoint(screenCenter + projection[i, j].Value - Vector2.One, color, 3f);
        }

        private IEnumerable<Vector3?[,]> CalculatePoints(int width, int height, Vector2 startPoint)
        {
            var points = new Vector3?[_renderFunctions.Length][,];
            (float startX, float startY) = startPoint;

            for (var i = 0; i < _renderFunctions.Length; i++)
            {
                points[i] = new Vector3?[width, height];
                for (var xi = 0; xi < width; xi++)
                {
                    for (var yi = 0; yi < height; yi++)
                    {
                        float
                            x = startX + _step * (xi - width / 2),
                            y = startY + _step * (yi - height / 2);

                        decimal? value = CalculatePointAt(i, (int) x, (int) y);

                        if (value.HasValue)
                        {
                            points[i][xi, yi] = new Vector3
                            {
                                X = x,
                                Y = y,
                                Z = (float) value.Value
                            };
                        }
                    }
                }
            }

            return points;
        }

        private decimal? CalculatePointAt(int i, float x, float y)
        {
            if (!_hash.ContainsKey((i, x, y)))
            {
                Symbol symbol = _renderFunctions[i][x][y];
                Symbol result = _context.Reduce(symbol);

                _hash[(i, x, y)] = result;
            }

            return (_hash[(i, x, y)] as Literal<decimal>)?.Value;
        }
    }
}