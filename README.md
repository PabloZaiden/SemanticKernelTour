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
    Hi! My name is Pablo
    What's my name?

To show context and history.

Then, ask: What's the weather like in my area?

It shouldn't know.

### step-3

It would be nice to know the current date and time.

Add function GetCurrentDateAndTime

How should we do to let the LLM use it?

### step-4

Add plugins to kernel, to be able to invoke the GetCurrentDateAndTime function

Explain that it's based on the function name and parameter names and types.
If it's not descriptive enough, you can also add descriptions to the function.

### step-5

(null)

### step-6
Now, let's add some more plugins to the kernel with different capabilities.

Add GeoData and Weather functions as plugins

### step-7

Change log level to information

Now, ask: 
    What's the weather like in my area?

Show that it chains function calls to retrieve all the data and answer the question.

### step-8

Add web search and browsing capabilities

Ask:
    Give me the latest news from where I live.
    
Now, it should search the web and navigate websites to find the answer.

### step-9      

Add Wolfram Alpha plugin, with custom LLM function to read the XML response

Show how the plugin works.
It retrieves the answer from Wolfram Alpha, and the LLM reads the XML response to get the answer.

Ask it to solve: 2x + 3 = 7

### step-10

Remove all plugins, now the LLM fails to answer "how many r are there in strawberry"

Show ChatGPT with o1 model. It should be able to answer the question, but it's much more expensive.

### step-11

Added plugin with specific ability. Now it knows how to answer using the right tool, but with a much cheaper model. The important thing is for the LLM to know when to use each tool.

### step-12

IFunctionInvocationFilter example, to render specific responses.

Show the Emotions plugin and class.
Show the filter.

Ask it: How do you feel?