using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;

public static class CsvHelperMethods
{
    public static List<AddCustomerDTO> ReadCsvFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new InvalidDataException("File is empty or not provided.");
        }

        using (var stream = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvReader(stream, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,  // Ignore missing fields
            HeaderValidated = null     // Ignore header validation
        }))
        {
            return csv.GetRecords<AddCustomerDTO>().ToList();
        }
    }
}

public class AddCustomerDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    // Assuming you have a Groups property, you might want to adjust this if your CSV doesn't include it
    public List<string> Groups { get; set; } = new List<string>();
}
