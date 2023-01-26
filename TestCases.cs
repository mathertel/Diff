using System;
using System.Text;
using my.utils;


// flags from the command line
bool verboseMode = false;
bool helpMode = false;

int countPassed = 0;
int countFailed = 0;

void TestHelper(string testName, string a, string b, string expect) {

  if (verboseMode) {
    Console.WriteLine($"Test2 {testName} ...");
    Console.WriteLine($"  a={a}");
    Console.WriteLine($"  b={b}");
  }

  Diff.Item[] f = Diff.DiffText(a.Replace(',', '\n'), b.Replace(',', '\n'), false, false, false);

  StringBuilder ret = new StringBuilder();
  for (int n = 0; n < f.Length; n++) {
    ret.Append(f[n].deletedA.ToString() + "." + f[n].insertedB.ToString() + "." + f[n].StartA.ToString() + "." + f[n].StartB.ToString() + "*");
  }

  if (verboseMode) Console.WriteLine($"  result={ret}");

  if (ret.ToString() == expect) {
    if (verboseMode) Console.WriteLine($"  passed");
    countPassed++;
  } else {
    Console.ForegroundColor = ConsoleColor.Red;
    if (verboseMode) {
      Console.WriteLine($"  failed.");
    } else {
      Console.WriteLine($" {testName} failed.");
    }
    Console.ResetColor();
    countFailed++;
  }
} // TestHelper()


Console.WriteLine("Diff Self Test...");

foreach (var arg in args) {
  Console.WriteLine($"Argument={arg}");
  if (arg == "-h") helpMode = true;
  if (arg == "--help") helpMode = true;
  if (arg == "--verbose") verboseMode = true;
  if (arg == "-v") verboseMode = true;
}

if (helpMode) {
  Console.WriteLine("Diff [-v] [-h]");
  Console.WriteLine("  -h show this help");
  Console.WriteLine("  -v enable verbose mode");
  Console.WriteLine();
}

// test all changes
TestHelper(
  "all-changes",
  "a,b,c,d,e,f,g,h,i,j,k,l",
  "0,1,2,3,4,5,6,7,8,9",
  "12.10.0.0*");

// test all same
TestHelper(
  "all-same",
  "a,b,c,d,e,f,g,h,i,j,k,l",
  "a,b,c,d,e,f,g,h,i,j,k,l",
  "");

// test snake
TestHelper(
  "snake",
  "a,b,c,d,e,f",
  "b,c,d,e,f,x",
  "1.0.0.0*0.1.6.5*");

// 2002.09.20 - repro
TestHelper(
  "repro20020920",
  "c1,a,c2,b,c,d,e,g,h,i,j,c3,k,l",
  "C1,a,C2,b,c,d,e,I1,e,g,h,i,j,C3,k,I2,l",
  "1.1.0.0*1.1.2.2*0.2.7.7*1.1.11.13*0.1.13.15*");

// 2003.02.07 - repro
TestHelper(
  "repro20030207",
  "F",
  "0,F,1,2,3,4,5,6,7",
  "0.1.0.0*0.7.1.2*");

// Muegel - repro
TestHelper(
  "repro20030409",
  "HELLO,WORLD",
  ",,hello,,,,world,",
  "2.8.0.0*");

// test some differences
TestHelper(
  "some-changes",
  "a,b,-,c,d,e,f,f",
  "a,b,x,c,e,f",
  "1.1.2.2*1.0.4.4*1.0.7.6*");

// test one change within long chain of repeats
TestHelper(
  "long chain",
  "a,a,a,a,a,a,a,a,a,a",
  "a,a,a,a,-,a,a,a,a,a",
  "0.1.4.4*1.0.9.10*");

Console.WriteLine($"{countPassed} test passed, {countFailed} test failed");

// End
