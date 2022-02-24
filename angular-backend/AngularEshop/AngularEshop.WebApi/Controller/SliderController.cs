using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularEshop.Core.Services.Interfaces;
using AngularEshop.Core.Utilities.Common;
using Microsoft.AspNetCore.Mvc;

namespace AngularEshop.WebApi.Controller
{
    public class SliderController : SiteBaseController
    {
        #region constructor
        private ISliderService sliderService;
        public SliderController(ISliderService sliderService)
        {
            this.sliderService = sliderService;
        }
        #endregion

        #region all active sliders
        [HttpGet("GetActiveSliders")]
        public async Task<IActionResult> GetActiveSliders()
        {
            var sliders = await sliderService.GetActiveSliders();
            return JsonResponseStatus.Success(sliders);
        }
        #endregion
    }
}
