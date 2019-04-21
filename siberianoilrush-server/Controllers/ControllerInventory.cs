using Microsoft.AspNetCore.Mvc;
using siberianoilrush_server.Extension.DBModel;
using siberianoilrush_server.Service;
using SorResources.Models.Static;
using System.Collections.Generic;
using System.Linq;
using Static = siberianoilrush_server.Database.Model.Static.Main;

namespace siberianoilrush_server.Controllers
{
    public abstract class ControllerInventory<TInventory, TStatic> : Controller
        where TInventory : Database.Postgre.Model.Inventory.OilfieldObject<TStatic>
        where TStatic : Static
    {
        protected readonly OilfieldObjectEssenceService<TInventory, TStatic> _inventoryEssenceService;

        public ControllerInventory(OilfieldObjectEssenceService<TInventory, TStatic> inventoryEssenceService)
        {
            _inventoryEssenceService = inventoryEssenceService;
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<TInventory> Get(long oilfieldId)
            => _inventoryEssenceService.GetByOilfieldId(oilfieldId);

        [Route("static")]
        [HttpGet]
        public virtual IEnumerable<StaticPurchasable> GetStatic()
            => _inventoryEssenceService.GetAllStatic().Select(x => x.Value.ToFrontend());
    }
}
