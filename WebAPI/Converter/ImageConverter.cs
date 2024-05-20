namespace WebAPI.Converter
{
    public class ImageConverter
    {
        public static string ImageToBase64(byte[] imageData)
        {
            return Convert.ToBase64String(imageData);
        }

        public static byte[] Base64ToImage(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }
    }
}
