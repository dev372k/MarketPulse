using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public static class CsvHelperMethods
{
    public static List<T> ReadCsvFile<T>(IFormFile file) where T : class
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
            var records = csv.GetRecords<T>().ToList();
            return records;
        }
    }
}
