using System.Collections.Generic;
using LabDiner.Restaurant.Humanoid;
using LabDiner.Restaurant.Manager;
using LabDiner.Restaurant.Model;
using LabDiner.Restaurant.Environment;
using LabDiner.Restaurant.Interface;
using LabDiner.Restaurant.SO;
using UnityEngine;

namespace LabDiner.Restaurant.Runtime
{
    /// <summary>
    /// Ngẫu nhiên lượng món khác nhau và số lượng từng món
    /// </summary>
    public class DefaultOrderComposer : IOrderComposer
    {
        private CoreStationRuntimeSO _coreStationRuntimeSO;

        public DefaultOrderComposer(CoreStationRuntimeSO coreStationRuntimeSO)
        {
            _coreStationRuntimeSO = coreStationRuntimeSO;
        }
        
        public Order Compose(GuestContext guest, int maxUniqueStations, int maxTotalQty)
        {
            Dictionary<CoreStation, int> orderDict =
                _coreStationRuntimeSO.GenerateRandomOrder(maxUniqueStations, maxTotalQty);

            return new Order(orderDict, guest, 0, false);
        }
    }
}