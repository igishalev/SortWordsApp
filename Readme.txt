This Program reads words from a specific file as per user request.
It reads the file and then create a new file Named - "F2.txt"
The content of the "F2.txt" file will be the distinct words sorted by an ascending or descening order 
based on user input.

In Addition the program will print on the console the most frequent word from the input file.
In case there are multiple words with the same maximum number of occurrences, they will all be printed.

EXAMPLE OF COMMANDS
--------
<CMD> SortWordsApp.exe "C:\F1.txt"
      "Enter sort option: 'a' for ascending, 'd' for descending:"The file F2 has been created.
The output file will appear in the working directory, its full path will be mentioned in the console.

Tests:
E2E flows to validate the integrity of the program.

Settings: appsettings.json
These settings can be used in your application to handle files differently based on their size.
For example, you might process small files in memory but use a different approach for large files to avoid memory issues.

  "FileSizeThresholds": 
  {
    "SmallFileThreshold": 10485760, // 10 MB in bytes 10 * 1024 * 1024
    "LargeFileThreshold": 104857600 // 100 MB in bytes 100 * 1024 * 1024
  }

#Looking forward to sorting with you!
