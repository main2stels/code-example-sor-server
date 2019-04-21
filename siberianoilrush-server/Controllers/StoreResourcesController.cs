using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using siberianoilrush_server.Extension.DBModel;
using siberianoilrush_server.Filters;
using siberianoilrush_server.Service.Inventory;
using siberianoilrush_server.Service.Public;
using SorResources.Models.Enums;
using SorResources.Models.Types;
using System.Collections.Generic;
using System.Linq;

namespace siberianoilrush_server.Controllers
{
    [GameLogicFilter]
    [Authorize]
    [Route("api/[controller]")]
    public class StoreResourcesController : Controller
    {
        private readonly IStoreResourcesService _storeResourcesService;
        private readonly IUserService _userService;
        private readonly IWalletService _walletService;

        public StoreResourcesController(IStoreResourcesService storeResourcesService,
            IUserService userService,
            IReadWalletService walletService)
        {
            _storeResourcesService = storeResourcesService;
            _userService = userService;
            _walletService = walletService;
        }

        [HttpGet]
        public IEnumerable<WalletModel> Get()
        {
            var user = _userService.GetById(User.Identity.Name);

            var result = _walletService.GetByUserId(user.Id);

            return result.Select(x => x.ToFrontend());
        }

        [HttpPost]
        public void Buy(ResourceType resourceType, int count)
        {
            var user = _userService.GetById(User.Identity.Name);

            _storeResourcesService.Buy(user.Id, resourceType, count);
        }

        [HttpPost]
        public void Sell(ResourceType resourceType, int count)
        {
            var user = _userService.GetById(User.Identity.Name);

            _storeResourcesService.Sell(user.Id, resourceType, count);
        }
    }
}
