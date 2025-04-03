using Microsoft.AspNetCore.Mvc;
using SalesAPI.Application.DTOs;
using SalesAPI.Application.Services;

namespace SalesController.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetAll()
        {
            var sales = await _saleService.GetAllAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetById(Guid id)
        {
            var sale = await _saleService.GetByIdAsync(id);
            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpGet("number/{saleNumber}")]
        public async Task<ActionResult<SaleDto>> GetBySaleNumber(string saleNumber)
        {
            var sale = await _saleService.GetBySaleNumberAsync(saleNumber);
            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetByCustomerId(Guid customerId)
        {
            var sales = await _saleService.GetByCustomerIdAsync(customerId);
            return Ok(sales);
        }

        [HttpGet("branch/{branchId}")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetByBranchId(Guid branchId)
        {
            var sales = await _saleService.GetByBranchIdAsync(branchId);
            return Ok(sales);
        }

        [HttpPost]
        public async Task<ActionResult<SaleDto>> Create([FromBody] CreateSaleDto createSaleDto)
        {
            var sale = await _saleService.CreateSaleAsync(createSaleDto);
            return CreatedAtAction(nameof(GetById), new { id = sale.Id }, sale);
        }

        [HttpPost("{saleId}/items")]
        public async Task<ActionResult<SaleDto>> AddItem(Guid saleId, [FromBody] AddItemDto addItemDto)
        {
            try
            {
                var sale = await _saleService.AddItemAsync(saleId, addItemDto);
                return Ok(sale);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{saleId}/cancel")]
        public async Task<ActionResult<SaleDto>> CancelSale(Guid saleId)
        {
            try
            {
                var sale = await _saleService.CancelSaleAsync(saleId);
                return Ok(sale);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{saleId}/items/{productId}/cancel")]
        public async Task<ActionResult<SaleDto>> CancelItem(Guid saleId, Guid productId)
        {
            try
            {
                var sale = await _saleService.CancelItemAsync(saleId, productId);
                return Ok(sale);
            }
            catch (ApplicationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}