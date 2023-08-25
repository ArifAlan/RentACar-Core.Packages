using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Paging
{
    public class Paginate<T>
    {
        public Paginate()
        {
            Items = Array.Empty<T>();// Boş bir eleman olarak kurguluyoruz 
        }

        public int Size { get; set; }//Bir sayfadaki öğe sayısını belirten bir özellik
        public int Index { get; set; }// Mevcut sayfanın indeksini belirten bir özellik (0'dan başlar).
        public int Count { get; set; }
        public int Pages { get; set; }
        public IList<T> Items { get; set; } //Sayfadaki öğeleri içeren bir IList<T> (genel bir liste) özelliği.
        //  HasPrevious : Mevcut sayfanın önceki bir sayfasının olup olmadığını belirten bir özellik
        // HasNext: Mevcut sayfanın sonraki bir sayfasının olup olmadığını belirten bir özellik.
        public bool HasPrevious => Index > 0;// Index değeri 0'dan büyükse (yani mevcut sayfa ilk sayfa değilse), bu özellik true değerini döndürür.
        //public bool HasPrevious
        //{
        //    get { return Index > 0; }
        //}
        public bool HasNext => Index + 1 < Pages; //Bu özellik, mevcut sayfanın sonraki bir sayfasının olup olmadığını belirten bir boolean değeri döndürür. Index + 1 ifadesi, mevcut sayfanın bir sonraki sayfasının indeksini temsil eder. Eğer bu değer Pages (toplam sayfa sayısı) değerinden küçükse, yani mevcut sayfa son sayfa değilse, bu özellik true döner. 
    }
}
