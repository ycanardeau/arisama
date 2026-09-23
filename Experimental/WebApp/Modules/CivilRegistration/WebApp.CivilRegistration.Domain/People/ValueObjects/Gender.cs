namespace WebApp.CivilRegistration.Domain.People.ValueObjects;

[GenerateMatch]
internal abstract record Gender
{
	public abstract bool CanBeHusband { get; }

	public abstract bool CanBeWife { get; }

	private Gender() { }

	public abstract bool CanMarryAtAge(Age age);

	public sealed record Male : Gender
	{
		/// <summary>
		/// The minimum marriageable age for males in Japan as of 2021.
		/// </summary>
		private static readonly Age MinimumMarriageableAge = new(18);

		public override bool CanBeHusband => true;

		public override bool CanBeWife => false;

		public override bool CanMarryAtAge(Age age)
		{
			return age >= MinimumMarriageableAge;
		}
	}

	public sealed record Female : Gender
	{
		/// <summary>
		/// The minimum marriageable age for females in Japan as of 2021.
		/// </summary>
		private static readonly Age MinimumMarriageableAge = new(16);

		public override bool CanBeHusband => false;

		public override bool CanBeWife => true;

		public override bool CanMarryAtAge(Age age)
		{
			return age >= MinimumMarriageableAge;
		}
	}
}
