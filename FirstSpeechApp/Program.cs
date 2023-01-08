using System;
using System.Collections.Generic;
using System.Speech.Recognition;
using System.Drawing;
using System.Speech.Synthesis.TtsEngine;

namespace FirstSpeechApp;

class Program
{
    static void Main(string[] args)
    {
        // Create a new SpeechRecognitionEngine
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();

        // Create a list of colors to recognize
        Choices colors = new Choices("red", "green", "blue");

        // Create a GrammarBuilder object and add the list of colors to it
        GrammarBuilder builder = new GrammarBuilder();
        builder.Append(colors);

        // Create a new Grammar object and load it into the recognition engine
        Grammar grammar = new Grammar(builder);
        engine.LoadGrammar(grammar);

        // Register for the SpeechRecognized event
        engine.SpeechRecognized += Engine_SpeechRecognized;

        // Set the recognition engine to listen for spoken input
        engine.SetInputToDefaultAudioDevice();

        // Start the recognition engine
        engine.RecognizeAsync(RecognizeMode.Multiple);

        // Keep the console window open until the recognition engine is stopped
        Console.ReadLine();
    }

    private static void Engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        Console.WriteLine(e.Result.Text);
        // Print the recognized color to the console
        switch (e.Result.Text)
        {
            case "red":
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Clear();
        break;
            case "green":
                Console.BackgroundColor = ConsoleColor.Green;
                Console.Clear();
                break;
            case "blue":
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Clear();
                break;
        }
    }
}
