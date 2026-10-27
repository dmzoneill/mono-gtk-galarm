# Galarm

<p align="center">
  <img src=".github/logo.png" alt="mono-gtk-galarm logo" width="128" />
</p>

A Mono GTK# alarm clock.

## What it does

Each alarm has a name, a time, the days of the week it repeats on, a volume, and
a program to run with its arguments, such as a media player. When an alarm goes
off Galarm sets the volume and starts that program. Alarms are saved as XML.

A source tarball, `galarm-0.1.tar.gz`, is included.

## Tech

- C# with Mono and GTK#

## Building

Open the `.sln` file in MonoDevelop, or build it with Mono and run the resulting
`.exe` with `mono`. Requires GTK# 2.

## License

Licensed under the Apache License 2.0. See [LICENSE](LICENSE).
