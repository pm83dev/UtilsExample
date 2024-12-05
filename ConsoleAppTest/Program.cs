using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

class Program
{
    static void Main(string[] args) // Metodo statico valido come punto di ingresso
    {
        // Creazione di un'istanza di ModelList e chiamata del metodo addElement
        ModelFuncClass modelListInstance = new ModelFuncClass();
        JsonSerDeserClass jsonSerDeserClass= new JsonSerDeserClass();
        modelListInstance.AddElement();
        modelListInstance.createDatatable();
        modelListInstance.CreateJson();
        modelListInstance.ReadJson();
        jsonSerDeserClass.SerJsonAnnidiate(modelListInstance.JsonOut);
    }
}

// Classe Model
public class ModelBase
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    // Sovrascrivi ToString per stampare i dettagli dell'oggetto
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Description: {Description}";
    }
}

// Classe ModelList
public class ModelFuncClass
{
    public List<ModelBase> ModelsList { get; set; } = new();
    public DataTable dataTable { get; set; } = new();
    public string ?JsonOut {get;set;}
    // Metodo per aggiungere elementi alla lista
    public void AddElement()
    {
        // Creazione degli oggetti Model
        ModelBase m1 = new ModelBase
        {
            Id = 0,
            Name = "nome1",
            Description = "test1"
        };

        ModelBase m2 = new ModelBase
        {
            Id = 1,
            Name = "nome2",
            Description = "test2"
        };

        // Aggiunta degli oggetti alla lista
        ModelsList.Add(m1);
        ModelsList.Add(m2);

        // Iterazione e stampa degli oggetti
        foreach (var m in ModelsList)
        {
            Console.WriteLine("Oggetti da lista"+m.ToString());
        }
    }

    public void createDatatable()
    {
        dataTable.Columns.Add(new DataColumn("Id", typeof(int)));
        dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
        dataTable.Columns.Add(new DataColumn("Description", typeof(string)));

        foreach (var item in ModelsList)
        {
            dataTable.Rows.Add(item.Id, item.Name, item.Description);    
        }
        
        foreach (DataRow row in dataTable.Rows)
        {
            Console.WriteLine("Da datatable:"+row["Name"]);
        }


    }

    public void CreateJson()
    {
        // Serializza ModelsList in una stringa JSON
        string json = JsonSerializer.Serialize(ModelsList, new JsonSerializerOptions
        {
            WriteIndented = true // Per una formattazione leggibile
        });

        // Stampa il JSON
        Console.WriteLine("File json"+json);
        JsonOut = json;
    }

    public void ReadJson()
    {
        // Deserializza il JSON in una lista di oggetti
            var modelList = JsonSerializer.Deserialize<List<ModelBase>>(JsonOut);

            if (modelList != null)
            {
                Console.WriteLine("Lista deserializzata:");
                foreach (var model in modelList)
                {
                    Console.WriteLine($"Id: {model.Id}, Name: {model.Name}, Description: {model.Description}");
                }
            }
            else
            {
                Console.WriteLine("Errore nella deserializzazione del JSON.");
            }
    }
}

// classi annidiate
public class ModelAnnidiate
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Details Details { get; set; }
    public List<string> Tags { get; set; }
}

public class Details
{
    public int Age { get; set; }
    public string Country { get; set; }
}

// serialize e deserialize con classi annidiate
public class JsonSerDeserClass()
{
    public List<ModelAnnidiate> ModelsListAnnidiate { get; set; } = new();
    public void SerJsonAnnidiate(string JsonString)
    {
        // Deserializza il JSON in un oggetto Model
        var model = JsonSerializer.Deserialize<ModelAnnidiate>(JsonString);

        if (model != null)
        {
            // Stampa i valori deserializzati
            Console.WriteLine($"Id: {model.Id}");
            Console.WriteLine($"Name: {model.Name}");
            Console.WriteLine($"Age: {model.Details.Age}");
            Console.WriteLine($"Country: {model.Details.Country}");
            Console.WriteLine("Tags:");
            foreach (var tag in model.Tags)
            {
                Console.WriteLine($"- {tag}");
            }
        }
    }


    public void DeSerJsonAnnidiate()
    {
        // Serializza l'oggetto di nuovo in JSON
        string serializedJson = JsonSerializer.Serialize(ModelsListAnnidiate, new JsonSerializerOptions
        {
            WriteIndented = true // Formattazione leggibile
        });
        Console.WriteLine("\nJSON serializzato:");
        Console.WriteLine(serializedJson);

    }


}
