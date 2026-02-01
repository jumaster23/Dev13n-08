namespace Okane.Application;

public class FileExpensesRepository : IRepository<Expense>
{
    private readonly string _path = "expenses.txt";
    
    public IEnumerable<Expense> All()
    {
        if (!File.Exists(_path)) return new List<Expense>();

        var lista = new List<Expense>();
        var lineas = File.ReadAllLines(_path);

        foreach (var linea in lineas)
        {
            var p = linea.Split(','); 
     
            lista.Add(new Expense 
            { 
                Id = int.Parse(p[0]), 
                Amount = int.Parse(p[1]), 
                CategoryName = p[2] 
            });
        }
        return lista;
    }

    public void Add(Expense entity)
    {
        var lista = All().ToList();
        int nuevoId = lista.Count > 0 ? lista.Max(e => e.Id) + 1 : 1;
        
        entity.Id = nuevoId;
        
        string nuevaLinea = $"{entity.Id},{entity.Amount},{entity.CategoryName}";
        File.AppendAllLines(_path, new[] { nuevaLinea });
    }

    public void Remove(int id)
    {
        var listaSinElBorrado = All().Where(e => e.Id != id).ToList();
        Save(listaSinElBorrado);
    }

    public Expense Update(int id, UpdateExpenseRequest request)
    {
        var lista = All().ToList();
        var index = lista.FindIndex(e => e.Id == id);
        
        var actualizado = new Expense 
        { 
            Id = id, 
            Amount = request.Amount, 
            CategoryName = request.CategoryName 
        };
        
        lista[index] = actualizado;
        Save(lista);
        return actualizado;
    }

    private void Save(List<Expense> lista)
    {
        var lineas = lista.Select(e => $"{e.Id},{e.Amount},{e.CategoryName}");
        File.WriteAllLines(_path, lineas);
    }

    public Expense? ById(int id) => All().FirstOrDefault(e => e.Id == id);
    public bool Exists(int id) => ById(id) != null;
}