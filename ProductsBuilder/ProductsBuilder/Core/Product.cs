using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ProductsBuilder.Core
{
    //Тип продукта
    enum ProductType
    {
        Weight,//весовой
        Piece//штучный
    }
    class Product
    {
        #region  private members
        // Идентификатор
        private int id;
        // Масса, стоимость, уточненная стоимость
        private double wt, price, exactPrice;
        //Наименование
        private string name;
        //Компоненты
        private HashSet<Product> components;
        //Параметры
        private Dictionary<string, object> pars;
        //Тип продукта 
        private ProductType productType;
        #endregion
        #region Конструкторы и инициализация
        /// <summary>
        /// Конструктор продукта
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="name">Наименование</param>
        /// <param name="type">Тип продукта</param>
        /// <param name="wt">Масса</param>
        /// <param name="price">Стоимость</param>
        /// <param name="pars">Параметры</param>
        /// <param name="components">Компоненты</param>
        public Product(int id,string name,ProductType type,double wt, double price,Dictionary<string,object> pars=null,IEnumerable<Product> components=null)
        {
            Init(id, name, type, wt, price, pars,components);
        }
        //Инициализация продукта
        private void Init(int id, string name, ProductType type, double wt, double price, Dictionary<string, object> pars, IEnumerable<Product> components)
        {
            this.id = id;
            this.name = name;
            this.productType = type;
            this.wt = wt;
            this.price = price;
            this.pars = pars;
            this.components = components.ToHashSet();

        }
        #endregion
        #region properties
        public int ID
        {
            get { return id; }
        }
        public double WT
        {
            get { return wt; }
        }
        public double Price
        {
            get { return exactPrice!=0.0?exactPrice:price; }
        }
        public double ExactPrice
        {
            get { return exactPrice; }
            set { exactPrice = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string[] ParamsNames
        {
            get { return pars==null?null:pars.Keys.ToArray(); }
        }
        public Product[] Components
        {
            get { return components.ToArray(); }
        }
        #endregion
        #region Public Methods
            #region Параметры
        /// <summary>
        /// Добавление параметра продукта
        /// </summary>
        /// <param name="paramName">Наименоване параметра</param>
        /// <param name="paramValue">Значение параметра</param>
        public void AddParam(string paramName,object paramValue)
        {
            if (String.IsNullOrEmpty(paramName))
            {
                return;
            }
            else if (pars == null)
            {
                pars = new Dictionary<string, object>();
            }
            if (!pars.ContainsKey(paramName))
            {
                pars.Add(paramName, paramValue);
            }
        }
        /// <summary>
        /// Установка параметра продукта
        /// </summary>
        /// <param name="paramName">Наименоване параметра</param>
        /// <param name="paramValue">Значение параметра</param>
        public void SetParam(string paramName, object paramValue)
        {
            if (String.IsNullOrEmpty(paramName))
            {
                return;
            }

            if (pars == null)
            {
                throw new NullReferenceException();
            }
            if (!pars.ContainsKey(paramName))
            {
                throw new Exception(string.Format("Parameter {0} not found!",paramName));
            }
            else
            {
                pars[paramName]=paramValue;
            }
        }
        /// <summary>
        /// Удаление параметра продукта
        /// </summary>
        /// <param name="paramName">Наименоване параметра</param>
        public void RemoveParam(string paramName)
        {
            if (String.IsNullOrEmpty(paramName))
            {
                return;
            }

            if (pars == null)
            {
                throw new NullReferenceException();
            }
            if (!pars.ContainsKey(paramName))
            {
                throw new Exception(string.Format("Parameter {0} not found!", paramName));
            }
            else
            {
                pars.Remove(paramName);
            }
        }

        #endregion
            #region Компоненты
        /// <summary>
        /// Добавление продукта в компоненты
        /// </summary>
        /// <param name="product">Продукт</param>
        public void AddComponent(Product product)
        {
            if (product == null)
            {
                throw new NullReferenceException();
            }
            if (components == null)
            {
                components = new HashSet<Product>();
            }

            components.Add(product);
        }
        /// <summary>
        /// Удаление продукта из компонентов
        /// </summary>
        /// <param name="product">Продукт</param>
        public void RemoveComponent(Product product)
        {
            if (product == null || components == null)
            {
                throw new NullReferenceException();
            }

            components.Remove(product);
        }
        #endregion
        #endregion
    }
}