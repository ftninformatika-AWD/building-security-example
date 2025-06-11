using BuildingExample.DTOs;
using BuildingExample.Exceptions;

namespace BuildingExample.Validators
{
    public class ApartmentValidator
    {
        public static void ValidateSearchApartment(ApartmentSearchDTO dto)
        { 
            if (dto.AreaFrom > dto.AreaTo)
            {
                throw new InvalidAreaBadRequestException();
            }
        }
    }
}
