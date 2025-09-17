namespace Bookstore.Client {
    using System;
    using System.Collections.Generic;

    public partial class Book {//Book.cs是自动生成的，这边新建BookPart2.cs，保持名称空间一致
        public string Report() {//使用partial类，自动生成代码变更也不会影响追加的代码
            return $"#{this.ID} Name:{this.Name} Price:{this.Price}";
        }
    }
}