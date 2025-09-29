using System.ComponentModel.DataAnnotations;

namespace school_electronic_magazine.Models;

public class Grade : BaseEntity
{
    public string grade { get; set; } // Тут тип перменной string, т.к у нас там может быть значение От (Отсутсвовал по ув. причине), Б (Болел) и тд, а не только оценки
}