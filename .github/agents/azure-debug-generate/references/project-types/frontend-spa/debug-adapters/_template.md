<!-- azure-cor-disclaimer -->
> **Important:** This skill provides guidance and recommended instructions to assist the AI system. Outputs are not guaranteed to be complete, correct, secure, or applicable to every scenario. Results should be reviewed and validated by a human before being applied. The AI model may choose not to follow all instructions exactly, and additional verification may be required.

# {Adapter} — Browser Debug Adapter

> **Template** — Copy to `{adapter}.md` for a new browser debug adapter.

## VS Code Debugger Type

| Browser | VS Code Debugger Type | Required Extension |
|---------|----------------------|-------------------|
| {browser} | `{type}` | {extension or "None — built-in"} |

---

## Launch Configuration

```json
{
  "name": "{id} (debug)",
  "type": "{type}",
  "request": "launch",
  "url": "http://localhost:{port from Framework Lookup Table}",
  "preLaunchTask": "{id} dev"
}
```

| Field | Source |
|-------|--------|
| `type` | VS Code Debugger Type above |
| `url` | Default Port in [frontend-spa.md § Framework Lookup Table](../frontend-spa.md) |
| `preLaunchTask` | `{id} dev` — dev server task from [frontend-spa.md § VS Code Task Configuration](../frontend-spa.md) |

<!-- Add adapter-specific fields and notes below. -->

---

## Notes

<!-- Document adapter behavior, extra fields, or required extensions here. -->
