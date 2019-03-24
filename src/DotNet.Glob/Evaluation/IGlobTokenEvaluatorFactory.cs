using DotNet.Globbing.Token;

namespace DotNet.Globbing.Evaluation
{
    public interface IGlobTokenEvaluatorFactory
    {
        IGlobTokenEvaluator CreateTokenEvaluator(AnyCharacterToken token);
        IGlobTokenEvaluator CreateTokenEvaluator(CharacterListToken token);
        IGlobTokenEvaluator CreateTokenEvaluator(LetterRangeToken token);
        IGlobTokenEvaluator CreateTokenEvaluator(LiteralToken token);
        IGlobTokenEvaluator CreateTokenEvaluator(NumberRangeToken token);
        IGlobTokenEvaluator CreateTokenEvaluator(PathSeparatorToken token);
        IGlobTokenEvaluator CreateTokenEvaluator(WildcardDirectoryToken token, CompositeTokenEvaluator nestedCompositeTokenEvaluator);
        IGlobTokenEvaluator CreateTokenEvaluator(WildcardToken token, CompositeTokenEvaluator nestedCompositeTokenEvaluator);

    }

}