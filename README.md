# An O(ND) Difference Algorithm for C\#

This is a C# implementation of the famous algorithm that finds the best diff of 2 inputs.
You can use it for text documents and compare the complete lines,
for a text lines and compare the characters or just to compare 2 arrays of numbers.
This algorithm is used by many applications that need to find the best way to describe the difference e.g. to extract it as a patch.

This implementation is based on the algorithm published in "An O(ND) Difference Algorithm and its Variations"
by Eugene Myers Algorithmica Vol. 1 No. 2, 1986, p 251.

The source code that you can find in the download implements a small class with
a simple to use API that just does this job. You should have it in the bag of your algorithms.

The core algorithm is comparing 2 arrays of numbers so when you like to find differences in text files you need a
small converter that is also included in the implementation because this is the most used purpose.

<!--
## TODO
* use <https://stackoverflow.com/questions/876656/difference-between-dictionary-and-hashtable>
-->


## Documentation and source code

A brief documentation on porting and adapting the algorithm to C# can be found in [Documentation](doku.md).

This is a detailed documentation inside the Diff class implementation
that explains some of the backgrounds, concepts and the API.

The Source code (<20 kByte for the class) is now availabe in this repo. It was originally published on
<https://www.codeproject.com/articles/13326/an-o-nd-difference-algorithm-for-c>
  and hosted on my provate web site <https: //www.mathertel.de/Diff />.
