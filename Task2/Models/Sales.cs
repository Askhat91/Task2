namespace Task2.Models
{
    /// <summary>
    /// Продажи
    /// </summary>
    public class Sales
    {
        /// <summary>
        /// Продажи
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Дата в формате ГГГГММДД (целое число)
        /// </summary>
        public int Date_Id { get; set; }
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public int GoodId { get; set; }
        /// <summary>
        /// Количество продаж
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Идентификатор аптеки
        /// </summary>
        public int PharmId { get; set; }
    }
}