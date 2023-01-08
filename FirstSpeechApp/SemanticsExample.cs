using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace FirstSpeechApp;

public class SemanticsExample
{
    void RunExample()
    {

        // Create a new SpeechRecognitionEngine
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();

        // Create a list of light names to recognize
        Choices lights = new Choices("light one", "light two", "light three");

        // Create a GrammarBuilder object and add the list of light names to it
        GrammarBuilder builder = new GrammarBuilder();
        builder.Append(lights);

        // Create a list of colors to recognize
        Choices colors = new Choices("red", "yellow", "green");

        // Add a semantic value to the list of colors
        colors.SetTag("color", "color");

        // Add the list of colors to the GrammarBuilder
        builder.Append(colors);

        // Create a new Grammar object and load it into the recognition engine
        Grammar grammar = new Grammar(builder);
        engine.LoadGrammar(grammar);

        // Set the recognition engine to listen for spoken input
        engine.SetInputToDefaultAudioDevice();

        // Start the recognition engine
        engine.RecognizeAsync(RecognizeMode.Multiple);

        // Set up a handler for the SpeechRecognized event
        engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(engine_SpeechRecognized);

        // Wait for the user to press a key before exiting the program
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    // Event handler for the SpeechRecognized event
    static void engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        // Get the recognized light name
        string lightName = e.Result.Text;

        // Get the semantic interpretation of the recognition result
        SemanticValue semantics = e.Result.Semantics;

        // Print the recognized light name and color to the console
        Console.WriteLine(lightName + ": " + semantics["color"].Value);

        // Perform an action based on the recognized light name and color
        switch (lightName)
        {
            case "light one":
                // Turn on light one with the specified color
                Console.WriteLine("Turning on light one with color " + semantics["color"].Value + "...");
                break;
            case "light two":
                // Turn on light two with the specified color
                Console.WriteLine("Turning on light two with color " + semantics["color"].Value + "...");
                break;
            case "light three":
                // Turn on light three with the specified color
                Console.WriteLine("Turning on light three with color " + semantics["color"].Value + "...");
                break;
        }
    }
}