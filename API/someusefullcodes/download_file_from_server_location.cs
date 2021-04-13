public IActionResult downloadfileAsync(string filepath, string filename)
        {
            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);

            }
            catch (Exception ex)
            {
                
                return NotFound();
            }
        }