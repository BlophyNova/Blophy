using System.Collections.Generic;

namespace UtilityCode.FileOperating.Types
{
    internal struct RootModel
    {
        public Metadata Metadata { get; set; }
        public Chart Chart { get; set; }
    }

    internal struct Metadata
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public Music Music { get; set; }
    }

    internal struct Music
    {
        public string Source { get; set; }
        public List<string> Author { get; set; }
        public double Bpm { get; set; }
        public double Offset { get; set; }
        public double Length { get; set; }
    }

    internal struct Chart
    {
        public Difficulty Easy { get; set; }
        public Difficulty Normal { get; set; }
        public Difficulty Hard { get; set; }
    }

    internal struct Difficulty
    {
        public List<string> Author { get; set; }
        public string Source { get; set; }
        public Illustration Illustration { get; set; }
        public Statistics Statistics { get; set; }
    }

    internal struct Illustration
    {
        public List<string> Author { get; set; }
        public string Source { get; set; }
    }

    internal struct Statistics
    {
        public int Tap { get; set; }
        public int Hold { get; set; }
        public int Drag { get; set; }
        public int Flick { get; set; }
        public int FullFlick { get; set; }
        public int Point { get; set; }
    }
}
