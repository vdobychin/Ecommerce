# Ecommerce
List<int> UnwindingFilter = new List<int>();
            if (Unwinding_30 != 0) UnwindingFilter.Add(Unwinding_30);
            if (Unwinding_100 != 0) UnwindingFilter.Add(Unwinding_100);
            ViewBag.products = UnwindingFilter.Any() ? db.Products.Where(i => UnwindingFilter.Contains(i.MonofilamentLine.Unwinding)) : db.Products;

            return View(shopCart);
