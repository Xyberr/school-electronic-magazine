using school_electronic_magazine.Data;

namespace school_electronic_magazine.Repositories;

public class SchoolClassRepository(AppDbContext context) : GenericRepository<Models.SchoolClass>(context) ,ISchoolClassRepository
{
    // тут пустой репозиторий нужен для ТЗ, пункт 3.1
}