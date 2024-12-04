using System;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;

class Program
{
    static void Main(string[] args) // Metodo statico valido come punto di ingresso
    {
        // Creazione di un'istanza di ModelList e chiamata del metodo addElement
        ModelList modelList = new ModelList();
        modelList.AddElement();
        modelList.createDatatable();
        modelList.CreateJson();
    }
}

// Classe Model
public class Model
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
public class ModelList
{
    public List<Model> ModelsList { get; set; } = new();
    public DataTable dataTable { get; set; } = new();
    // Metodo per aggiungere elementi alla lista
    public void AddElement()
    {
        // Creazione degli oggetti Model
        Model m1 = new Model
        {
            Id = 0,
            Name = "nome1",
            Description = "test1"
        };

        Model m2 = new Model
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
            Console.WriteLine(m.ToString());
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
            Console.WriteLine(row["Name"]);
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
    Console.WriteLine(json);
}
}
