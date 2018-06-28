# WordCounter
WordCounter library and test app

KHT 20180627
This solution contains two projects: WordCounter and WordCounterTest.
WordCounter compiles as a library (WordCounter.dll) and WordCounterTest compiles as a WinForms app (WordCounterTest.exe).
WordCounterTest is the startup project.
WordCounterTest has a reference to WordCounter.dll.

WordCounter.dll is a library with one public method: WordCounter.GetKeywordCount(List<String> keywords, String url).
This method returns the number of times any of the keywords appear in the resource identified by the URL.
The URL can be a local file resource or a web resource.
Only full-word matches are sought, and the search is case-sensitive.
The WordCounter will cache results for up to one hour.
The WordCounter is built for a multi-threaded environment.

To test the WordCounter, compile both projects and run WordCounterTest.exe, or start directly from Visual Studio.
Enter a comma-delimited list (no spaces) of keywords and a valid URL in the form and click Test.
A successful search will display the results on the form.

The folder and file structure is as follows:

..\WordCounter\
 WordCounter.cs
 WordCounter.csproj
 WordCounter.sln
 Properties\AssemblyInfo.cs

..\WordCounterTest\
 App.config
 Main.cs
 Main.Designer.cs
 Main.resx
 Program.cs
 WordCounterTest.csproj
 Properties\AssemblyInfo.cs
 Properties\Resources.Designer.cs
 Properties\Resources.resx
 Properties\Settings.Designer.cs
 Properties\Settings.settings
