CREATE TABLE ShopCartItem (
    Id         INTEGER PRIMARY KEY,
    Price      DECIMAL,
    Quantity   INTEGER,
    ShopCartId STRING,
    ProductId  INT
);