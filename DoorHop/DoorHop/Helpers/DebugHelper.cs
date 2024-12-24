using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public static class DebugHelper
{
    private static Texture2D debugTexture;

    public static void Initialize(GraphicsDevice graphicsDevice)
    {
        debugTexture = new Texture2D(graphicsDevice, 1, 1);
        debugTexture.SetData(new[] { Color.White });
    }

    public static void DrawBounds(SpriteBatch spriteBatch, Rectangle bounds, Color color)
    {
        #if DEBUG
        if (debugTexture != null)
        {
            System.Diagnostics.Debug.WriteLine($"Drawing debug bounds at: {bounds}");  // Debug bericht
            spriteBatch.Draw(debugTexture, bounds, color);
        }
        #endif
    }

    public static void Dispose()
    {
        debugTexture?.Dispose();
        debugTexture = null;
    }
} 