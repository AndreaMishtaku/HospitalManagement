using Shared.DTO.Request;
using Shared.DTO.Response;
using Shared.ResponseFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IService;

public interface IEquipmentService
{

    Task<BaseResponse> CreateEquipment(EquipmentRequestDTO equipmentDTO);

    Task<EquipmentResponseDTO> GetEquipmentById(int id);

    Task<IEnumerable<EquipmentResponseDTO>> GetAllEquipments();

    Task<BaseResponse> UpdateEquipment(int id, EquipmentRequestDTO equipmentDTO);

    Task<BaseResponse> DeleteEquipment(int id);
}
