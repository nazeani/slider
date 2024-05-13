using Pronia.Business.Exceptions;
using Pronia.Business.Services.Abstracts;
using Pronia.Core.Models;
using Pronia.Core.RepositoryAbstracts;
using Pronia.Data.RepositoryConcrates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pronia.Business.Services.Concrates
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        public SliderService(ISliderRepository sliderRepository)
        {
            _sliderRepository=sliderRepository; 
        }

        public void Add(Slider slider)
        {
            if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                throw new ImageContentTypeException("Fayl formati duzgun deyil!");
            if(slider.ImageFile.FileName.Length > 2097152)
                throw new ImageSizeException("en cox 2 mb yer tutlamlidir!");
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(slider.ImageFile.FileName);
                fileName=fileName.Length+ slider.ImageFile.FileName.Length >100 ? slider.ImageFile.FileName.Substring(0,58) +fileName : fileName+ slider.ImageFile.FileName;
                string path = "C:\\Users\\Nazani\\OneDrive\\Masaüstü\\9may\\8may\\wwwroot\\" + "uploads\\sliders\\" + fileName;
                using(FileStream fileStream=new FileStream(path,FileMode.Create))
                {
                    slider.ImageFile.CopyTo(fileStream);
                }
                slider.ImageUrl=fileName;
                _sliderRepository.Add(slider);
                _sliderRepository.Commit();
            

        }

        public void Delete(int id)
        {
            
        }

        public Slider Get(Func<Slider, bool> func = null)
        {
            return _sliderRepository.Get(func);
        }

        public List<Slider> GetAll(Func<Slider, bool> func = null)
        {
            return _sliderRepository.GetAll(func);
        }

        public void Update(int? id, Slider slider)
        {
          
        }
    }
}
