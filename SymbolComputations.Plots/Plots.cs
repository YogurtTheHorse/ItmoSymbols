using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGame.Extended.Shapes;
using SymbolComputations.Reducers;
using SymbolComputations.Symbols;

namespace SymbolComputations.Plots
{
    public class Plots : Game
    {
        private readonly Symbol _renderSymbol;
        private readonly ReductionContext _context;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector3? _startPoint;

        public Plots(Symbol renderSymbol, ReductionContext context)
        {
            _renderSymbol = renderSymbol;
            _context = context;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _startPoint = Formula(10) ?? Vector3.Zero;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        private Vector3? Formula(float x)
        {
            Symbol symbol = _context.Reduce(_renderSymbol[x]);
            var identifier = symbol as Identifier;
            
            return identifier switch
            {
                { Name: "Point" } => new Vector3(GetPointCoordinate(0), GetPointCoordinate(1), GetPointCoordinate(2)),
                
                _ => (Vector3?)null
            };

            float GetPointCoordinate(int i) => 
                identifier.Tail.Count < i 
                    ? 0 
                    : (float) ((identifier.Tail[i] as Literal<decimal>)?.Value ?? 0);
        }
    }
}