namespace BE.Domain.Entities
{
    public class Provider
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        private Provider() { }

        public Provider(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}


