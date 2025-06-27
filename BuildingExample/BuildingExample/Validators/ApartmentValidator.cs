using BuildingExample.DTOs;
using BuildingExample.Exceptions;

namespace BuildingExample.Validators
{
    // manuelna validacija putem Validator klase koja je smeštena u Validators paket
    public class ApartmentValidator
    {
        // kada je neophodno izvršiti validaciju objekta na način koji nije podržan
        // Data anotacijama, najjednostavniji način je napraviti Validator klasu sa
        // metodama koje vrše validaciju na način koji mi želimo i pozivati ih manuelno
        public static void ValidateSearchApartment(ApartmentSearchDTO dto)
        { 
            if (dto.AreaFrom > dto.AreaTo)
            {
                // ako validacija nije uspešna, baciti izuzetak koji sadrži razlog
                // zašto je validacija neuspešna
                throw new InvalidAreaBadRequestException();
            }
        }
    }
}
