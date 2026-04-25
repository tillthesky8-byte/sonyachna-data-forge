---
description: "Expand concise phase notes into structured implementation plans"
name: "Phase Builder"
agent: "agent"
argument-hint: "Phase name and a concise implementation summary"
---
Expand the provided phase note into an elaborated phase plan for this repository.

Follow the plan-reading rules from [overview.md](../../.docs/overview.md):
- Use an H1 for the phase title
- Use H2 for steps
- Use H3 for implementations
- Preserve and distribute every implementation item from the input
- Separate distinct concerns into distinct steps when that makes the plan more even
- Include commit messages where a step or implementation boundary is implied
- Use the repository's keyword style for classes, methods, records, interfaces, enums, and types

Output requirements:
- Return only the expanded phase plan
- Keep the wording concrete and actionable
- Do not add unrelated scope
- Keep the result balanced across steps instead of leaving one overloaded section

If the input is very terse, infer the smallest reasonable structure and expand it into a clear phase breakdown without changing the intent.