## Helper class for plugins developing
- Change Namespace
## Discord logs helper
- То что нужно засунуть в кфг
```csharp
public string DiscordWebHook { get; set; } = "";
public string DiscordAuthor { get; set; } = "Author(Server name for example)";
public Dictionary<string, EmbedModel> Embeds { get; set; } = new()
{
    {"example key", new EmbedModel("Title", "description", new ColorModel(0, 255, 0), new []{ new FieldModel("example field name", "field content", false) })}
};
```
- OnConfigParsed:
```csharp
DiscordLogsHelper.WebHook = Config.DiscordWebHook;
DiscordLogsHelper.Author = Config.DiscordAuthor;
DiscordLogsHelper.Embeds = Config.Embeds;
```
