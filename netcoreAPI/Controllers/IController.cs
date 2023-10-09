using netcoreAPI.Dal;
using netcoreAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace netcoreAPI.Controllers
{
    public interface IController
    {
        public abstract Task<IActionResult> GetAll();
    }
}