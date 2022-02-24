using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.DataLayer.Entities.Site;
using AngularEshop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;

namespace AngularEshop.Core.Services.Implementations
{
    public class SliderService : ISliderService
    {
        #region constructor
        private IGenericRepository<Slider> sliderRepository;

        public SliderService(IGenericRepository<Slider> sliderRepository)
        {
            this.sliderRepository = sliderRepository;
        }
        #endregion

        #region slider
        public async Task AddSlider(Slider slider)
        {
            await sliderRepository.AddEntity(slider);
            await sliderRepository.SaveChanges();
        }

        public async Task<List<Slider>> GetActiveSliders()
        {
            return await sliderRepository.GetEntitiesQuery().Where(s => !s.IsDelete).ToListAsync();
        }

        public async Task<List<Slider>> GetAllSliders()
        {
            return await sliderRepository.GetEntitiesQuery().ToListAsync();
        }

        public async Task<Slider> GetSliderById(long sliderId)
        {
            return await sliderRepository.GetEntityById(sliderId);
        }

        public async Task UpadteSlider(Slider slider)
        {
            sliderRepository.UpdateEntity(slider);
            await sliderRepository.SaveChanges();
        }
        #endregion

        #region dispose
        public void Dispose()
        {
            this.sliderRepository?.Dispose();
        }
        #endregion
    }
}
