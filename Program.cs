using System;
using System.Collections.Generic;

namespace FactoryMethod
{
    public enum Actions
    {
        ShipperUPS,
        ShipperFedEx,
        ShipperAramax
    }

    public interface IShip
    {
        string WhoAmI();
    }

    public abstract class ShipperBase : IShip
    {
        //Etc
        public string MyName { get; set; }
        virtual public string WhoAmI()
        {
            return null;
        }
    }

    public class ShipperUPS : ShipperBase
    {
        override public string WhoAmI()
        {
            MyName = "I'm UPS";
            return MyName;
        }
    }

    public class ShipperFedEx : ShipperBase
    {
        override public string WhoAmI()
        {
            MyName = "I'm FedEx";
            return MyName;
        }
    }

    public class ShipperAramax : ShipperBase
    {
        override public string WhoAmI()
        {
            MyName = "I'm Aramax";
            return MyName;
        }
    }
    public interface IShipperFactory
    {
        IShip CreateInstance(Actions enumModuleName);
    }
    //public class ShipperFactory : IShipperFactory
    //{
    //    public ShipperFactory()
    //    {

    //    }

    //    public static readonly IDictionary<Shipper, Func<IShip>> Creators = new Dictionary<Shipper, Func<IShip>>()
    //        {
    //        { Shipper.UPS, () => new ShipperUPS() },
    //        { Shipper.FedEx, () => new ShipperFedEx() },
    //        { Shipper.Aramax, () => new ShipperAramax() }
    //        };

    //    public IShip CreateInstance(Shipper enumModuleName)
    //    {
    //        return Creators[enumModuleName]();
    //    }

    //}

    public class ShipperFactory : IShipperFactory
    {
        public readonly IDictionary<Actions, IShip> _factories;
        public ShipperFactory()
        {
            _factories = new Dictionary<Actions, IShip>();
            foreach (Actions action in Enum.GetValues(typeof(Actions)))
            {
                string objectToInstantiate = "FactoryMethod." + action + ", FactoryMethod";
                var objectType = Type.GetType(objectToInstantiate);
                IShip factory = (IShip)Activator.CreateInstance(objectType);
                _factories.Add(action, factory);
            }
        }


        public IShip CreateInstance(Actions action)
        {
            return _factories[action];
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var shipperFactory = new ShipperFactory();
            var shipper =shipperFactory.CreateInstance(Actions.ShipperFedEx);
            Console.WriteLine(shipper.WhoAmI());
        }
    }
}
