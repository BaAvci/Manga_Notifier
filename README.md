# Geo_Assessment
[![Test](https://github.com/Jisatzu/Manga_Notifier/actions/workflows/CI-Test.yml/badge.svg?branch=master)](https://github.com/Jisatzu/Manga_Notifier/actions/workflows/CI-Test.yml)

Manga Notifier is a C# project that crawls websites and notfies the user if a certain Manga / Manwha / Manhua (named Comics in the project) got a new chapter. The user can select for which comic, which translator and which notifications method he wants to get notified. 

## Usage

### Prerequisites
1. .Net 6.0 installed  
2. SSMS installed
3. Ability to connect to following server: "SERVER:Localhost"

### Preparation
1. Create all needed tables in database
2. Add URL for comic page
3. Create new class for nonexisting Translator
4. Add XPath code in new class

### Execution
1. Run project

### Result  
- Notification through prefered method