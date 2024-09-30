# SemanticKernel Tour

Sample repo to show SemanticKernel capabilities.

## How to use

Go to the first commit, and move to each of the branches, following the steps.

## Script

### step-0

Project ready to code

### step-1

Create new kernel using OpenAI provider

### step-2

Create a simple chatbot app.
Test is with:
```
Hi! My name is Pablo
What's my name?
```

To show context and history.

Then, ask: 
```
What time is it?
```

It shouldn't know.

### step-3

It would be nice to know the current date and time.

Add function GetCurrentDateAndTime

How should we do to let the LLM use it?

### step-4

Add plugins to kernel, to be able to invoke the GetCurrentDateAndTime function

Explain that it's based on the function name and parameter names and types.
If it's not descriptive enough, you can also add descriptions to the function.

Run it, and ask again: 
```
What time is it?
```
Now, it should know.

### step-5

(null)

### step-6
Now, let's add some more plugins to the kernel with different capabilities.

Add GeoData and Weather functions as plugins

### step-7

Change log level to information

Now, ask:
```
What's the weather like in my area?
```

Show that it chains function calls to retrieve all the data and answer the question.

### step-8

Add web search and browsing capabilities

Ask:
```
Give me the latest news from where I live.
```
    
Now, it should search the web and navigate websites to find the answer.

### step-9      

Add Wolfram Alpha plugin, with custom LLM function to read the XML response

Show how the plugin works.
It retrieves the answer from Wolfram Alpha, and the LLM reads the XML response to get the answer.

Ask:
```
solve 2x + 3 = 7
```

### step-10

Remove all plugins, now the LLM fails to answer: 
```
how many r are there in strawberry
```

Show ChatGPT with o1 model. It should be able to answer the question, but it's much more expensive.

### step-11

Added plugin with specific ability. 

Ask again:
```
how many r are there in strawberry
```

Now it knows how to answer using the right tool, but with a much cheaper model. The important thing is for the LLM to know when to use each tool.

### step-12

`IFunctionInvocationFilter` example, to render specific responses.

Show the Emotions plugin and class.
Show the filter.

Ask it:
```
How do you feel?
```

It should open a file with the emotion, in addition to the text response

## Other topics to mention

- It's not all sunshine and rainbows:
    - The longer the context, the less effective the LLM will be
    - Especially important if it's including several available tools
        - Possible workaround, use a function to make it choose which tools it should have available.
- OpenAPI based plugins
    - Provide an openapi definition, and it will import the endpoint as KernelFunctions
- Multimodal inputs
    - If the underlying model supports it, you can include pictures in the chat history
    - A great example is to retrieve context based on what the user is seeing on screen
        - Downside: it can become really expensive, really fast
    
- Memory support
    - Embeddings based
    - Several backends (including in-memory for local dev)
    - Can be used with templates for prompting, to retrieve memory data based on vector proximity:
        ```csharp
            var template = @"
                Information about me, from previous conversations:
                - {{$fact1}} {{recall $fact1}}
                - {{$fact2}} {{recall $fact2}}
                - {{$fact3}} {{recall $fact3}}
                - {{$fact4}} {{recall $fact4}}
                - {{$fact5}} {{recall $fact5}}
                ";

            var arguments = new KernelArguments();

            arguments["fact1"] = "what is my name?";
            arguments["fact2"] = "where do I live?";
            arguments["fact3"] = "where is my family from?";
            arguments["fact4"] = "where have I travelled?";
            arguments["fact5"] = "what do I do for work?";
        ```

- Ollama support
    - Still a work-in-progress
    - To be really useful, another model with good tools support is needed
    - Phi3 (or some newer version) will be an ideal candidate
        - As long as it's a good tool selector, doesn't matter if its a Small Language Model.
    - When ready, will unlock powerful local-llm scenarios at much cheaper cost
        - In fact... we could defer the inference to the client browser and the cost goes to $0!

- Several new features coming on Ignite (November 2024)
