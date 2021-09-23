using Homework_1_BurcuMantar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Homework_1_BurcuMantar.Controllers
{
    [Route("api/v1/doctors")]
    [ApiController]
    public class DoctorController : ControllerBase
    { 
        //This list represent our database
        public static List<DoctorDto> drList = new List<DoctorDto>()
            {
                new DoctorDto(){Id=1,FirstName="Ali",LastName="Yılmaz", Gender="M",HospitalName="A ",Clinic="Genel Cerrahi"},
                new DoctorDto(){Id=2,FirstName="Canan",LastName="Gök", Gender="F",HospitalName="A ",Clinic="Pediatri"},
                new DoctorDto(){Id=3,FirstName="Cemil",LastName="Girin", Gender="M",HospitalName="B ",Clinic="Dahiliye"},
                new DoctorDto(){Id=4,FirstName="Ece",LastName="Bulut", Gender="N",HospitalName="B ",Clinic="Dahiliye"},
                new DoctorDto(){Id=5,FirstName="Ece",LastName="Bilgin", Gender="F",HospitalName="B ",Clinic="Pediatri"},
                new DoctorDto(){Id=6,FirstName="Volkan",LastName="Liman", Gender="M",HospitalName="C ",Clinic="Göz Hastalıkları"},
                new DoctorDto(){Id=7,FirstName="Alkan",LastName="Kan", Gender="M",HospitalName="C ",Clinic="Göz Hastalıkları"},
            };


        //You can use the HttpGet request to take all drlist, and also you can take dr list with using specific filternames as Firstnama,lastname,hospital etc.
        //https://localhost:44345/api/v1/doctors or https://localhost:44345/api/v1/doctors?filter=firstName
        [HttpGet]
        public IActionResult Get(string filter)
        {
            
            if (string.IsNullOrEmpty(filter))
            {
                return Ok(drList);
            }
            else
            {
                string keyword = filter.ToLower();
                List<string> filteredList = new List<string>();
                switch (keyword)
                {
                    case "firstname":
                        foreach (var dr in drList)
                        {
                            filteredList.Add(dr.FirstName.ToString());
                        }
                        return Ok(filteredList);

                    case "lastname":
                        foreach (var dr in drList)
                        {
                            filteredList.Add(dr.LastName.ToString());
                        }
                        return Ok(filteredList);

                    case "gender":
                        foreach (var dr in drList)
                        {
                            filteredList.Add(dr.Gender.ToString());
                        }
                        return Ok(filteredList);
                    case "clinic":
                        foreach (var dr in drList)
                        {
                            filteredList.Add(dr.Clinic.ToString());
                        }
                        return Ok(filteredList);
                    case "hospitalname":
                        foreach (var dr in drList)
                        {
                            filteredList.Add(dr.HospitalName.ToString());
                        }
                        return Ok(filteredList);
                    default:
                        return Ok(drList);
                }
            }
            
        }

        //You can use this HttpGet request to make specific search , for example to get the dr objects who name is Ece (?name=ece )
        //This api users should use only one parameter in the one query. Otherwise user gets badrequest error.
        //https://localhost:44345/api/v1/doctors/list?name=Ece OR https://localhost:44345/api/v1/doctors/list?hospitalname=A
        [Route("list")]
        [HttpGet]  
        public IActionResult GetListedDr([FromQuery] string name, [FromQuery] string lastname, [FromQuery] string gender, [FromQuery] string clinic, [FromQuery] string hospitalname)
        {

            List<DoctorDto> filteredDrList = new List<DoctorDto>();
            List<string> parameters = new List<string>() { name, lastname, gender, clinic, hospitalname };
            int count = 0;
            foreach (var item in parameters)
            {
                if (item != null)
                    count++;
            }

            if (count > 1)
            {
                return BadRequest("Can not enter more than 1 paramater value.");
            }

            if (!string.IsNullOrEmpty(name))
            {
                foreach (var dr in drList)
                {
                    if ((dr.FirstName.ToLower().Contains(name.ToLower())))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }
            else if (!string.IsNullOrEmpty(lastname))
            {
                foreach (var dr in drList)
                {
                    if (dr.LastName.ToLower().Contains(lastname.ToLower()))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }
            else if (!string.IsNullOrEmpty(gender))
            {
                foreach (var dr in drList)
                {
                    if (dr.Gender.ToLower().Contains(gender.ToLower()))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }
            else if (!string.IsNullOrEmpty(clinic))
            {
                foreach (var dr in drList)
                {
                    if (dr.Clinic.ToLower().Contains(clinic.ToLower()))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }
            else if (!string.IsNullOrEmpty(hospitalname))
            {
                foreach (var dr in drList)
                {
                    if (dr.HospitalName.ToLower().Contains(hospitalname.ToLower()))
                    {
                        filteredDrList.Add(dr);
                    }
                }
                return Ok(filteredDrList);
            }
            else
            {
                return BadRequest("There is not any parameter value to list. ");
            }

        }

        //You can use this HttpGet request to sort dr list with name(1),lastname(2),gender(3),clinic(4),hospital(5)
        //https://localhost:44345/api/v1/doctors/sort?value=1 OR https://localhost:44345/api/v1/doctors/sort?value=4
        [Route("sort")]
        [HttpGet]  
        public IActionResult GetSortedDr(SortEnum value)
        {
            switch (value)
            {
                case SortEnum.name:
                    return Ok(drList.OrderBy(x => x.FirstName));
                case SortEnum.lastname:
                    return Ok(drList.OrderBy(x => x.LastName));
                case SortEnum.gender:
                    return Ok(drList.OrderBy(x => x.Gender));
                case SortEnum.clinic:
                    return Ok(drList.OrderBy(x => x.Clinic));
                case SortEnum.hospitalname:
                    return Ok(drList.OrderBy(x => x.HospitalName));
                default:
                    return BadRequest();
            }

        }

        //You can use this HttpGet request with id  to get just one dr object.
        //https://localhost:44345/api/v1/doctors/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDr(int id)
        {
            DoctorDto doctor = drList.FirstOrDefault(x => x.Id.Equals(id));
            if (doctor == null)
            {
                return NotFound();
            }
            return await Task.FromResult(Ok(doctor));
        }

        //You can use this HttpPost request to create new dr's object.
        //https://localhost:44345/api/v1/doctors
        [HttpPost]
        public IActionResult CreateDr([FromBody] DoctorDto dr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            dr.Id = drList.Max(x => x.Id) + 1;
            drList.Add(dr);
            return CreatedAtAction("GetDr", new { Id = dr.Id }, dr); //api/doctors/6
        }

        //You can use this HttpPut request to update dr's object already have with using unique id.
        //https://localhost:44345/api/v1/doctors/8
        [HttpPut("{id}")]
        public IActionResult UpdateDr([FromRoute] int id, [FromBody] DoctorDto dr)
        {
            if (ModelState.IsValid)
            {
                if (id != dr.Id)
                {
                    return BadRequest("Id information is not confirmed");
                }
                if (!drList.Any(x => x.Id.Equals(id)))
                {
                    return NotFound();
                }
                drList.Remove(drList[id - 1]);
                drList.Insert((id - 1), dr);
                return Ok();
            }
            return BadRequest(ModelState);

        }

        //You can use this HttpDelete request to delete dr's object already have with using unique id.
        //https://localhost:44345/api/v1/doctors/8
        [HttpDelete("{id}")]
        public IActionResult DeleteDr(int id)
        {
            var dr = drList.FirstOrDefault(x => x.Id.Equals(id));
            if (dr == null)
            {
                return NotFound();
            }
            drList.Remove(dr);
            return NoContent();
        }

        //You can use this HttpPacth request to update one field from dr object's  fields already have with using unique id and UpdateHospitalNameDto.cs class.
        //https://localhost:44345/api/v1/doctors/1
        [HttpPatch("{id}")]
        public IActionResult UpdateHospital([FromRoute] int id, [FromBody] UptadeHospitalNameDto name)
        {
            if (ModelState.IsValid)
            {
                var dr = drList.FirstOrDefault(x => x.Id.Equals(id));
                if (dr == null)
                {
                    return NotFound();
                }
                foreach (var doctor in drList)
                {
                    if (id.Equals(doctor.Id))
                    {
                        doctor.HospitalName = name.HospitalName;
                        return Ok();
                    }
                }
            }
            return BadRequest(ModelState);

        }

    }
}
