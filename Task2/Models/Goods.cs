namespace Task2.Models
{
    /// <summary>
    /// Товары
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// Идентификатор производителя
        /// </summary>
        public int ProducerId { get; set; }
    }
}