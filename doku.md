# An O(ND) Difference Algorithm for C\#

This article is about comparing text files and the proven, best and most famous algorithm to identify the differences between them. The source code that you can find in the download implements a small class with a simple to use API that just does this job. You should have it in the bag of your algorithms.

The algorithm was first published 35 Years ago under "An O(ND) Difference Algorithm and its Variations" by Eugene Myers Algorithmica Vol. 1 No. 2, 1986, p 251 . You can find a copy if it at <http://www.xmailserver.org/diff2.pdf>.

In this article you can find a abstract recursive definition of the algorithm using some pseudo-code that needs to be transferred to an existing programming language.

## History and Why

There are many C, Java, Lisp implementations public available of this algorithm out there on the internet. Before I wrote the C# version I discovered that almost all these implementations seem to come from the same source (GNU diffutils) that is only available under the (unfree) GNU public license and therefore cannot be reused as a source code for a commercial or re-distributable application without being trapped by the GNU license.

There are very old C implementations that use other (worse) heuristic algorithms. Microsoft also published source code of a diff-tool (windiff) that uses some tree structures. Also, a direct transfer from a C source to C# is not easy because there is a lot of pointer arithmetic in the typical C solutions and I wanted a managed solution. I tried a lot sources but at least found no usable solution written for the .NET platform.

These are the reasons why I implemented the original published algorithm from the scratch and made it available without the GNU license limitations under a BSD style license. The history of this implementation is back to 2002 when I published a Visual Studio add-in that also can compare files, see <http://www.codeproject.com/KB/macros/WebReports8.aspx>.

I found no more bugs in the last 15 years so I think that the code is stable.

I did not need a high performance diff tool. I will do some performance tweaking when needed, so please let me know.
I also dropped some hints in the source code on that topic.

## How it works (briefely)

You can find a online working version at <http://www.mathertel.de/>.

* Comparing the characters of 2 huge text files is not easy
  to implement and tends to be slow.
  Comparing numbers is much easier so the first step is to compute unique numbers for all textlines.
  If textlines are identical then identical numbers are computed.

* There are some options before computing these numbers that normally are usefull for some kind of text: stripping off space characters and comparing case insensitive.

* The core algorithm itself will compare 2 arrays of numbers and the preparation is done in the private DiffCodes method and by using a Hashtable.

* The methods `DiffText` and `DiffInt` can be used for simple cases.

* The core of the algorithm is built using 2 methods:

  **LCS** -- This is the divide-and-conquer implementation of the longes common-subsequence algorithm.

  **SMS** -- This method finds the Shortest Middle Snake.

* To get some usable performance I did some changes to the original algorithm.
  The original algorithm was described using a recursive approach and comparing zero indexed sequences and passes parts of these sequences as parameters.
  Extracting sub-arrays and rejoining them is very CPU and memory intensive.
  To avoid copying these sequences as arrays around the used arrays together with the lower and upper bounds are passed while the sequences are not copied around all the time.
  This circumstance makes the LCS and SMS functions more complicate.

* I added some code to the LCS function to get a fast response on sub-arrays that are identical, completely deleted or inserted
  instead of recursively analysing them down to the single number case.

* The result is stored in 2 arrays that flag for modified (deleted or inserted) lines in the 2 data arrays.
  These bits are then analyzed to produce an array of objects that describe the found differences.

* Read the original article if you want to understand more.

## The API

To use this functionality you only have to call one of the DiffText methods. They all get a pair of strings and return an array of items that describe the inserts and deletes between the 2 strings. There are no "changes" reported. Instead you can find a "insert" and "deleted" pair.

`DiffText(string TextA, string TextB)`

Find the difference in 2 texts, comparing by textlines without any conversion. A array of Items containing the differences is returned.

`DiffText(string TextA, string TextB, bool trimSpace, bool ignoreSpace, bool ignoreCase)`

Find the difference in 2 texts, comparing by textlines with some optional conversions. A array of Items containing the differences is returned.

`Diff(int[] ArrayA, int[] ArrayB)`

Find the difference in 2 arrays of integers. A array of Items containing the differences is returned.

## A Test program

The TestCases.cs can be started on the command line with `dotnet run` and executes several test cases.

A very pragmatic unit test collection.


## Change Log

This work was first published at <http://www.gotdotnet.com/Community/UserSamples/Details.aspx?SampleGuid=> 96065252-BE1D-4E2F-B7CB-3695FEB0D2C3.

* **2002.09.20** There was a "hang" in some situations.

  Now I understand a little bit more of the SMS algorithm.

  There have been overlapping boxes; that where analyzed partial differently. One return-point is enough.

  A assertion was added in CreateDiffs when in debug-mode, that counts the number of equal (no modified) lines in both arrays. They must be identical.

* **2003.02.07** Out of bounds error in the Up/Down vector arrays in some situations.

  The two vectors are now accessed using different offsets that are adjusted using the start k-Line.

  A test case is added.

* **2003.04.09** Another test that throwed an exception was found,
  but already seems to be fixed by the 2002.09.20 work.

* **2006.03.10** Refactored the API to static methods on the Diff class
  to make usage simpler.

* **2023.01.23** re-published on Github <https://github.com/mathertel/Diff>
* using dotnet version 7
* tests now in TestCases.cs

## License

This work is licensed under a BSD 3-Clause "New" or "Revised" License. See [LICENSE](LICENSE).
