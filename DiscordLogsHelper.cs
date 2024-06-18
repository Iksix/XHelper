using Discord.Webhook;

namespace IksAdmin_VoteBKM;

public static class DiscordLogsHelper
{
    public static string WebHook { get; set; } = "";
    public static string Author { get; set; } = "";
    public static Dictionary<string, EmbedModel> Embeds = new();
    public static async Task SendToDiscord(string embedKey, Dictionary<string, string> replaces)
    {
        if (!Embeds.TryGetValue(embedKey, out var embed)) return;
        if (WebHook == "") return;
        try
        {
            var webhookObject = new WebhookObject();
            webhookObject.AddEmbed(builder =>
            {
                var embedTitle = embed.Title;
                var embedDescription = embed.Description;
                foreach (var replace in replaces)
                {
                    embedTitle = embedTitle.Replace("{" + $"{replace.Key}" + "}", replace.Value);
                    embedDescription = embedDescription.Replace("{" + $"{replace.Key}" + "}", replace.Value);
                }
                builder
                    .WithColor(new DColor(embed.Color.R, embed.Color.G, embed.Color.B))
                    .WithTitle(embedTitle)
                    .WithAuthor(Author)
                    .WithDescription(embedDescription);
                foreach (var field in embed.Fields)
                {
                    var fieldName = field.Name;
                    var fieldValue = field.Value;
                    foreach (var replace in replaces)
                    {
                        fieldName = fieldName.Replace("{" + $"{replace.Key}" + "}", replace.Value);
                        fieldValue = fieldValue.Replace("{" + $"{replace.Key}" + "}", replace.Value);
                    }
                    builder.AddField(field.Name, fieldValue, field.InLine);
                }
            });
            await new Webhook(WebHook).SendAsync(webhookObject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

public class EmbedModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ColorModel Color { get; set; }
    public FieldModel[] Fields { get; set; }

    public EmbedModel(string title, string description, ColorModel color, FieldModel[] fields)
    {
        Title = title;
        Description = description;
        Color = color;
        Fields = fields;
    }

    public FieldModel[] GetFields()
    {
        List<FieldModel> fields = new();
        foreach (var f in Fields)
        {
            fields.Add(new FieldModel(f.Name, f.Value, f.InLine));
        }

        return fields.ToArray();
    }
    public string GetTitle()
    {
        string title = Title.ToString();

        return title;
    }
    public string GetDescription()
    {
        string title = Description.ToString();

        return title;
    }
}

public class FieldModel
{
    public string Name { get; set; }
    public string Value { get; set; }
    public bool InLine { get; set; }

    public FieldModel(string name, string value, bool inLine = false)
    {
        Name = name;
        Value = value;
        InLine = inLine;
    }
}
public class ColorModel
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }

    public ColorModel(int r, int g, int b)
    {
        R = r;
        G = g;
        B = b;
    }
}
