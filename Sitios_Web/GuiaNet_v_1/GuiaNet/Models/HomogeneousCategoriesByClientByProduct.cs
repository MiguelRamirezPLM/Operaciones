using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaNet.Models
{
    public class HomogeneousCategoriesByClientByProduct
    {
        public int? CategoryIdL1 {get;set;}
		public string CategoryNameL1 {get;set;}
		public byte? LevelL1 {get;set;}
		
		public int? CategoryIdL2 {get;set;}
		public string CategoryNameL2 {get;set;}
		public byte? LevelL2 {get;set;}
		
		public int? HomogeneousCategoryIdL3 {get;set;}
		public string HomogeneousCategoryL3 {get;set;}
		public byte? LevelL3 {get;set;}
		public bool? ActiveL3 {get;set;}
		
		public int? LeafCategoryIdL4 {get;set;}
		public string LeafCategoryL4 {get;set;}
		public byte? LevelL4 {get;set;}
        public bool? Active4 { get; set; }
    }
}