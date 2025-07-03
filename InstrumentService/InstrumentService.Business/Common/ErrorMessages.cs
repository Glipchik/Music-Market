namespace InstrumentService.Business.Common;

public static class ErrorMessages
{
    public static string UserIdIsMissing => "User ID is missing.";
    public static string ForbiddenAction => "You do not have permission to perform this action.";
    public static string InstrumentNotFound(string id) => $"Instrument with id: {id} was not found";
    public static string FailedToSaveInstrument => "Failed to save instrument to the database.";
    public static string BaseFormMetadataNotFound => "Base form field definitions not found (ID: 'base').";

    public static string SpecificFormMetadataNotFound(string instrumentType) =>
        $"Specific form field definitions for '{instrumentType}' not found.";

    public static string FailedToResolveInstrumentStrategy(string instrumentId, string instrumentType) =>
        $"Failed to resolve strategy to handle instrument with id '{instrumentId}' and type '{instrumentType}'.";

    public static string FailedToFetchInstrumentStats(string instrumentId) =>
        $"Failed to fetch stats for instrument {instrumentId}.";
}