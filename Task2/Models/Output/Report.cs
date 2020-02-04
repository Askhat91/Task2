using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.Models.Output
{
    /// <summary>
    /// Отчет
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Дата
        /// </summary>
        public int Date { get; set; }
        /// <summary>
        /// Наименование аптеки
        /// </summary>
        public string PharmName { get; set; }
        /// <summary>
        /// Наименование категории 
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// Наименование производителя
        /// </summary>
        public string ProducerName { get; set; }
        /// <summary>
        /// Сумма продаж
        /// </summary>
        public decimal Sum { get; set; }
    }
}