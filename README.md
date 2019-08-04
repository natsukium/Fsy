# Fsy #bbbcde
Fsy [fzi] is a Brainf*ck interpreter written in F#.

## Requirements (for developers)

- .NET Core > 3.0 preview6

## Usage
Please download executable file from latest release.

```
./Fsy hello.bf
```

Input (hello.bf)
```
>+++++++++[<++++++++>-]<.>+++++++[<++++>-]<+.+++++++..+++.[-]>++++++++[<++++>-]<.>+++++++++++[<+++++>-]<.>++++++++[<+++>-]<.+++.------.--------.[-]>++++++++[<++++>-]<+.[-]++++++++++.
```

Output
```
Hello World!
```

## Build
If you want to get binary for other runtime:
```
cd src/Fsy
dotnet publich -c release -r <rid> --self-contained true /p:PublishTrimmed=true /p:ReadyToRun=true
```

If you want to get FDD binary (run Fsy on .NET Core):
```
cd src/Fsy
dotnet publish -c release -r <rid> --self-contained false
```

## LICENSE
MIT
