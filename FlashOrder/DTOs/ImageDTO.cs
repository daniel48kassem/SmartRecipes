namespace FlashOrder.DTOs
{
    public class CreateImageDTO
    {
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }

    public class ImageDTO : CreateImageDTO
    {
        public int Id { get; set; }
    }
}