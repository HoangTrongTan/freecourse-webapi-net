using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MywebApi.Models;

namespace MywebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        public static List<HangHoa> hanghoas = new List<HangHoa>();

        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(hanghoas);
        }
        [HttpGet("{id}")]
        public IActionResult get(string id) {
            try
            {
                var hanghoa = hanghoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }
                return Ok(hanghoa);
            }
            catch
            {
                return BadRequest();
            }
            
        }
        [HttpPost]
        public IActionResult Create(HangHoaVM hangHoavm) {
            var hanghoa = new HangHoa
            {
                MaHangHoa = Guid.NewGuid(),
                TenHangHoa = hangHoavm.TenHangHoa,
                DonGia = hangHoavm.DonGia,
            };
            hanghoas.Add(hanghoa);
            return Ok(new {
                Success = true, Data = hanghoa
            });
        }
        [HttpPut("{id}")]
        public IActionResult Update(string id, HangHoa hanghoaEdit)
        {
            try
            {
                var hanghoa = hanghoas.SingleOrDefault(hh => hh.MaHangHoa == Guid.Parse(id));
                if (hanghoa == null)
                {
                    return NotFound();
                }
                if(id != hanghoa.MaHangHoa.ToString())
                {
                    return BadRequest();
                }
                //Updates
                hanghoa.TenHangHoa = hanghoaEdit.TenHangHoa;
                hanghoa.DonGia = hanghoaEdit.DonGia;

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var hanghoa = hanghoas.SingleOrDefault( hh => { 
                    return hh.MaHangHoa == Guid.Parse(id);
                } );
                hanghoas.Remove(hanghoa);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
