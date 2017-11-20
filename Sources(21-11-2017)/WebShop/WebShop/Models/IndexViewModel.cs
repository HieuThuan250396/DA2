using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebShop.Models.Db;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebShop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<CUSTOMER> dsKH { get; set; }
        public Pager Pager { get; set; }
        public IEnumerable<USER> dsUSER { get; set; }
        public IEnumerable<INVENTORY> dsSP { get; set; }
        public IEnumerable<STAFF> dsNV { get; set; }
        public IEnumerable<VENDOR> dsVenDor { get; set; }
        public IEnumerable<PURCHASEORDER> dsPurchase { get; set; }
    }
    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }
}