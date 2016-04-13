using System;
using System.ComponentModel;
using LinqToExcel.Attributes;

namespace OurMemory.Service.Model
{
    public class VeteranMapping
    {
        [ExcelColumn("Фамилия")]
        public string FirstName { get; set; }
        [ExcelColumn("Имя")]
        public string LastName { get; set; }
        [ExcelColumn("Отчество")]
        public string MiddleName { get; set; }
        [ExcelColumn("Дата рождения")]
        public string DateBirth { get; set; }
        [ExcelColumn("Место рождения")]
        public string BirthPlace { get; set; }
        [ExcelColumn("Дата смерти")]
        public string DateDeath { get; set; }
        [ExcelColumn("Дата призыва")]
        public string Called { get; set; }
        [ExcelColumn("Награды")]
        public string Awards { get; set; }
        [ExcelColumn("Войска")]
        public string Troops { get; set; }
        [ExcelColumn("Описание")]
        public string Description { get; set; }
        [ExcelColumn("Ссылки на изображения")]
        public string UrlImages { get; set; }
    }
}